"use client";

import React, { createContext, useState, useContext, useEffect } from "react";
import axios from "axios";
import { useRouter } from "next/navigation";

const API = process.env.NEXT_PUBLIC_API_BASE;
const AuthContext = createContext<any>(null);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<any | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    fetchCurrentUser();
  }, []);

  const login = async (inputvalue: string, password: string) => {
    const res = await axios.post(`${API}/auth/login`, { inputvalue, password }, { withCredentials: true });
    const { token, userId } = res.data.result;

    setToken(token);

    // Busca usuário completo após login
    await fetchCurrentUser();
    return userId;
  };

  const logout = async () => {
    await axios.post(`${API}/auth/logout`, {}, { withCredentials: true });
    setUser(null);
    setToken(null);
    router.push("/");
  };

  const fetchCurrentUser = async () => {
    try {
      const res = await axios.get(`${API}/auth/me`, { withCredentials: true });

      // 🔑 Inclui roles do usuário
      const { userId, email, roles } = res.data.result;
      setUser({ userId, email, roles });
    } catch (err) {
      console.error("Erro ao buscar usuário atual", err);
      setUser(null);
    } finally {
      setLoading(false);
    }
  };

  return (
    <AuthContext.Provider value={{ user, token, login, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
