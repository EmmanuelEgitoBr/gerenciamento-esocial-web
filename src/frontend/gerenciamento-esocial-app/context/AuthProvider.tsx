"use client"; // importante: habilita hooks no App Router

import React, { createContext, useState, useContext, useEffect } from "react";
import axios from "axios";
import Cookies from "js-cookie";

const API = process.env.NEXT_PUBLIC_API_BASE;
const AuthContext = createContext<any>(null);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [user, setUser] = useState<any>(null);
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    const t = Cookies.get("token");
    const u = Cookies.get("user");
    if (t) setToken(t);
    if (u) setUser(JSON.parse(u));
  }, []);

  const login = async (username: string, password: string) => {
    const res = await axios.post(`${API}/auth/login`, { username, password }, { withCredentials: true });
    const { token, user } = res.data;
    Cookies.set("token", token);
    Cookies.set("user", JSON.stringify(user));
    setToken(token);
    setUser(user);
    return user;
  };

  const logout = () => {
    Cookies.remove("token");
    Cookies.remove("user");
    setUser(null);
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ user, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
