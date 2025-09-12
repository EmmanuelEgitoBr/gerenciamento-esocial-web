"use client";

import React, { createContext, useState, useContext, useEffect } from "react";
import axios from "axios";
import { useRouter } from "next/navigation";

const API = process.env.NEXT_PUBLIC_API_BASE;
const AuthContext = createContext<any>(null);

// Configuração global do axios (vai anexar token sempre que existir no localStorage)
axios.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<any | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [loading, setLoading] = useState(true);
  const router = useRouter();

  useEffect(() => {
    // Recupera token do localStorage ao carregar a aplicação
    const storedToken = localStorage.getItem("token");
    if (storedToken) {
      setToken(storedToken);
      fetchCurrentUser(storedToken);
    } else {
      setLoading(false);
    }
  }, []);

  const login = async (inputvalue: string, password: string) => {
    const res = await axios.post(`${API}/auth/login`, { inputvalue, password });

    const { token, userId } = res.data.result;

    // salva token no estado e no localStorage
    setToken(token);
    localStorage.setItem("token", token);

    // busca usuário com o token
    await fetchCurrentUser(token);

    return userId;
  };

  const logout = async () => {
    try {
      await axios.post(`${API}/auth/logout`, {}, { withCredentials: true });
    } catch {
      // se backend não suportar logout via cookie, apenas limpa o state
    }
    setUser(null);
    setToken(null);
    localStorage.removeItem("token");
    router.push("/");
  };

  const fetchCurrentUser = async (tokenOverride?: string) => {
    try {
      const res = await axios.get(`${API}/auth/me`, {
        headers: {
          Authorization: `Bearer ${tokenOverride || token}`,
        },
      });

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
