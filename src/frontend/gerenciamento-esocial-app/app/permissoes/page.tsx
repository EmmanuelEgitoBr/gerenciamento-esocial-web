"use client";

import React, { useEffect, useState } from 'react'
import { NavBar } from '../components/NavBar/NavBar'
import { useAuth } from '../../context/AuthProvider'
import { getPermissoes, getUsuarios, addUsuarioToRole } from '../lib/api'
import Link from 'next/link'

export default function Roles() {
    const { loading } = useAuth()
    const [permissoes, setPermissoes] = useState<any[]>([])
    const [modalOpen, setModalOpen] = useState(false);
    const [usuarios, setUsuarios] = useState<any[]>([]);
    const [selectedEmail, setSelectedEmail] = useState("");
    const [selectedRole, setSelectedRole] = useState<string | null>(null);
    const [message, setMessage] = useState<{ type: "success" | "error"; text: string } | null>(null);

    useEffect(() => {
        if (!loading) {
            fetchList()
        }
    }, [loading])

    const fetchList = async () => {
        try {
            const res = await getPermissoes()
            setPermissoes(res.data.result || [])
        } catch (e) {
            setPermissoes([])
        }
    }

    const openModal = async (role: string) => {
        setSelectedRole(role);
        setModalOpen(true);
        setMessage(null);
        setSelectedEmail("");
        try {
            const res = await getUsuarios();
            setUsuarios(res.data.result || []);
        } catch {
            setUsuarios([]);
        }
    };

    const handleAssociar = async () => {
        if (!selectedRole || !selectedEmail) return;
        try {
            console.log('Role: ', selectedRole);
            console.log('Email', selectedEmail);
            await addUsuarioToRole(selectedRole, selectedEmail);
            setMessage({ type: "success", text: "Usuário associado com sucesso!" });
        } catch (err: any) {
            setMessage({
                type: "error",
                text: err.response?.data?.message || "Erro ao associar usuário.",
            });
        }
    };

    const closeModal = () => {
        setModalOpen(false);
        setMessage(null);
        setSelectedEmail("");
    };

    const hasCadastro = permissoes.length > 0

    return (
        <div>
            <NavBar />
            <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100">
                <div className="p-8 bg-white rounded-2xl shadow-md w-96">
                    <div className="flex justify-between items-center mb-4">
                        <h1 className="text-2xl">Permissões Cadastradas</h1>
                    </div>
                    <br />
                    <br />

                    {!hasCadastro && <div>Não há cadastro disponível</div>}

                    {hasCadastro && (
                        <table className="min-w-full bg-white">
                            <thead>
                                <tr>
                                    <th className="p-2 text-center" style={{ color: '#013A73' }}>Permissão</th>
                                    <th className="p-2 text-center" style={{ color: '#013A73' }}>Adicionar Usuário</th>
                                </tr>
                            </thead>
                            <tbody>
                                {permissoes.map((p) => (
                                    <tr key={p} className="border-t">
                                        <td className="p-2 text-center">{p}</td>
                                        <td className="text-center">
                                            <button
                                                onClick={() => openModal(p)}
                                                className="btn btn-sm btn-outline-primary cursor-pointer"
                                                title="Adicionar permissão para usuário"
                                            >
                                                <i className="bi bi-plus-square-fill text-blue-900"></i>
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    )}
                    <div className="mt-4 p-4 bg-yellow-100 border-l-4 border-yellow-500 text-yellow-800 rounded">
                        Caso queira adicionar uma nova permissão, entrar em contato com o suporte de TI no email:{" "}
                        <a
                            href="mailto:suporte@company.com.br"
                            className="underline font-semibold"
                        >
                            suporte@company.com.br
                        </a>
                    </div>
                </div>
            </div>

            {/* Modal */}
            {modalOpen && (
                <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
                    <div className="bg-white rounded-lg shadow-lg p-6 w-96">
                        <h2 className="text-xl font-semibold mb-4">
                            Associar usuário à permissão: <span className="text-blue-700">{selectedRole}</span>
                        </h2>

                        {usuarios.length === 0 ? (
                            <p className="text-gray-600">Nenhum usuário disponível.</p>
                        ) : (
                            <select
                                value={selectedEmail}
                                onChange={(e) => setSelectedEmail(e.target.value)}
                                className="w-full border rounded px-3 py-2 mb-4"
                            >
                                <option value="">Selecione um usuário</option>
                                {usuarios.map((u: any) => (
                                    <option key={u.email} value={u.email}>
                                        {u.email}
                                    </option>
                                ))}
                            </select>
                        )}

                        {message && (
                            <div
                                className={`mb-3 p-2 rounded text-sm ${message.type === "success"
                                    ? "bg-green-100 text-green-700"
                                    : "bg-red-100 text-red-700"
                                    }`}
                            >
                                {message.text}
                            </div>
                        )}

                        <div className="flex justify-end gap-2">
                            <button
                                onClick={closeModal}
                                className="px-4 py-2 rounded bg-gray-400 text-white cursor-pointer"
                            >
                                Cancelar
                            </button>
                            <button
                                onClick={handleAssociar}
                                disabled={!selectedEmail}
                                className="px-4 py-2 rounded bg-blue-600 text-white disabled:bg-gray-400 cursor-pointer"
                            >
                                Associar Permissão
                            </button>
                        </div>
                    </div>
                </div>
            )}

        </div>
    )
}