"use client";

import React, { createContext, useState, useContext } from "react";
import axios from "axios";

const API = process.env.NEXT_PUBLIC_API_BASE;
const AuthContext = createContext<any>(null);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [userId, setUserId] = useState<string | null>(null);
  const [token, setToken] = useState<string | null>(null);

  const login = async (inputvalue: string, password: string) => {
    const res = await axios.post(
      `${API}/auth/login`,
      { inputvalue, password },
      { withCredentials: true } // importantíssimo
    );

    console.log(res.data);
    console.log(res.data.success);

    const { token, userId } = res.data.result;
    setToken(token);
    setUserId(userId);

    return res.data.result.userId;
  };

  const logout = async () => {
    // Se o back tiver endpoint de logout, chama aqui
    await axios.post(`${API}/auth/logout`, {}, { withCredentials: true });
  };

  return (
    <AuthContext.Provider value={{ userId, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
