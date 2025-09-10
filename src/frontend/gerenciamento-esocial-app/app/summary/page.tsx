"use client";

import React, { useEffect, useState } from "react";
import { NavBar } from "../components/NavBar/NavBar";
import { useAuth } from "../../context/AuthProvider";
import { getTrabalhadorByUserId } from "../lib/api";
import { isNullOrEmpty } from "@/utils/string-utils";
import Link from "next/link";

export default function Dashboard() {
  const { user, loading } = useAuth();
  const [trabalhador, setTrabalhador] = useState<any | null>(null);
  const [fetching, setFetching] = useState(false);
  const userId = user?.userId;

  useEffect(() => {
    if (!loading && userId) {
      fetchTrabalhador(userId);
    }
  }, [loading, userId]);

  const fetchTrabalhador = async (userId: string) => {
    setFetching(true);
    try {
      const res = await getTrabalhadorByUserId(userId);
      setTrabalhador(res.data.result || null); // recebe apenas 1 registro
      console.log(res.data.result)
    } catch (err) {
      console.error("Erro ao buscar trabalhador:", err);
      setTrabalhador(null);
    } finally {
      setFetching(false);
    }
  };

  const hasCadastro = trabalhador !== null;

  return (
    <div>
      <NavBar />
      <div className="p-6">
        <div className="flex justify-between items-center mb-4">
          <h1 className="text-2xl">Seu Cadastro</h1>
          {!hasCadastro && (
            <Link href="/trabalhador/create">
              <button
                className="px-4 py-2 rounded bg-blue-600 text-white cursor-pointer"
                disabled={hasCadastro}
              >
                Novo cadastro
              </button>
            </Link>
          )}
        </div>

        {!hasCadastro && <div>Não há cadastro disponível... favor cadastrar</div>}

        {hasCadastro && trabalhador && (
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="p-2 text-center">Nome</th>
                <th className="p-2 text-center">Data de Cadastro</th>
                <th className="p-2 text-center">Última Atualização</th>
                <th className="p-2 text-center">Status</th>
                <th className="p-2">Ações</th>
              </tr>
            </thead>
            <tbody>
              <tr key={trabalhador.trabalhadorId} className="border-t">
                <td className="p-2">{trabalhador.nome}</td>
                <td className="p-2 text-center">{new Date(trabalhador.dataCadastro).toLocaleDateString()}</td>
                <td className="p-2 text-center">
                  {trabalhador.dataUltimaAtualizacao
                    ? new Date(trabalhador.dataUltimaAtualizacao).toLocaleDateString()
                    : "-"}
                </td>
                <td className="p-2 text-center">{trabalhador.statusCadastro}</td>
                <td className="text-center">
                  <div className="btn-group gap-2">
                    <Link href={`/trabalhador/${trabalhador.trabalhadorId}/view`}>
                      <button className="btn btn-sm btn-outline-primary cursor-pointer" title="Ver">
                        <i className="bi bi-eye"></i>
                      </button>
                    </Link>
                    <Link href={`/trabalhador/${trabalhador.trabalhadorId}/edit`}>
                      <button className="btn btn-sm btn-outline-secondary cursor-pointer" title="Editar">
                        <i className="bi bi-pencil"></i>
                      </button>
                    </Link>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        )}

        {fetching && <div>Carregando...</div>}
      </div>
    </div>
  );
}
