"use client";

import { useState } from "react";

import { NavBar } from '../../components/NavBar/NavBar'
import { useAuth } from '../../../context/AuthProvider'
import { createUsuario } from "@/app/lib/api";

export default function CreateUser() {
    const [form, setForm] = useState({
        username: "",
        email: "",
        cpf: "",
        password: "",
    });
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);
        setSuccess(null);
        setLoading(true);

        try {
            const res = await createUsuario(form);

            if (res.data.isSuccess) {
                setSuccess("Usuário cadastrado com sucesso!");
                setForm({ username: "", email: "", cpf: "", password: "" });
            }
        } catch (err: any) {
            setError(
                err.response?.data?.message ||
                "Erro ao cadastrar. Verifique os dados e tente novamente."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <NavBar />
            <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100">
                <form onSubmit={handleSubmit}
                    className="p-8 bg-white rounded-2xl shadow-md w-96"
                >
                    <h2 className="text-2xl font-semibold mb-6 text-center">
                        Cadastro de Usuário
                    </h2>

                    {error && <p className="text-red-600 mb-3">{error}</p>}
                    {success && <p className="text-green-600 mb-3">{success}</p>}

                    <label className="block mb-2 text-sm">Usuário</label>
                    <input
                        type="text"
                        name="username"
                        value={form.username}
                        onChange={handleChange}
                        required
                        className="border rounded px-3 py-2 w-full mb-3"
                    />

                    <label className="block mb-2 text-sm">Email</label>
                    <input
                        type="email"
                        name="email"
                        value={form.email}
                        onChange={handleChange}
                        required
                        className="border rounded px-3 py-2 w-full mb-3"
                    />

                    <label className="block mb-2 text-sm">CPF</label>
                    <input
                        type="text"
                        name="cpf"
                        value={form.cpf}
                        onChange={handleChange}
                        required
                        className="border rounded px-3 py-2 w-full mb-3"
                    />

                    <label className="block mb-2 text-sm">Senha</label>
                    <input
                        type="password"
                        name="password"
                        value={form.password}
                        onChange={handleChange}
                        required
                        className="border rounded px-3 py-2 w-full mb-3"
                    />

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full py-2 rounded bg-blue-600 text-white hover:bg-blue-700 disabled:bg-gray-400 cursor-pointer"
                    >
                        {loading ? "Cadastrando..." : "Cadastrar"}
                    </button>
                </form>
            </div>
        </div>
    );
}