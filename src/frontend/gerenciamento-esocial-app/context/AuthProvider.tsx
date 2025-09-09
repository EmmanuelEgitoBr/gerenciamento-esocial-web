"use client";

import React, { createContext, useState, useContext } from "react";
import axios from "axios";

const API = process.env.NEXT_PUBLIC_API_BASE;
const AuthContext = createContext<any>(null);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<any>(null);

  const login = async (inputvalue: string, password: string) => {
    const res = await axios.post(
      `${API}/auth/login`,
      { inputvalue, password },
      { withCredentials: true } // importantíssimo
    );

    console.log(res.data);

    // Se o back mandar `user` no body, você pode setar no estado:
    if (res.data.result) {
      setUser(res.data.result.userid);
    }
    console.log('UserId: ', user);
    return res.data.result.userid;
  };

  const logout = async () => {
    // Se o back tiver endpoint de logout, chama aqui
    await axios.post(`${API}/auth/logout`, {}, { withCredentials: true });

    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
