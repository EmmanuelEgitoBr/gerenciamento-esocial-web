"use client";

import React, { useEffect, useState } from 'react'
import { NavBar } from '../components/NavBar/NavBar'
import { useAuth } from '../../context/AuthProvider'
import { getTrabalhadores } from '../lib/api'
import Link from 'next/link'

export default function Dashboard() {
  const { user, loading } = useAuth()
  const [trabalhadores, setTrabalhadores] = useState<any[]>([])
  
  useEffect(() => {
    if (!loading) {
      fetchList() 
    }
  }, [loading])

  const fetchList = async () => {
    try {
      const res = await getTrabalhadores()
      setTrabalhadores(res.data.result || [])
    } catch (e) {
      setTrabalhadores([])
    }
  }

  const hasCadastro = trabalhadores.length > 0

  return (
    <div>
      <NavBar />
      <div className="p-6">
        <div className="flex justify-between items-center mb-4">
          <h1 className="text-2xl">Lista de Cadastros Realizados</h1>
        </div>
        <br/>
        <hr/>

        {!hasCadastro && <div>Não há cadastro disponível</div>}

        {hasCadastro && (
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Nome</th>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Data de Cadastro</th>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Última Atualização</th>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Status</th>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Ações</th>
              </tr>
            </thead>
            <tbody>
              {trabalhadores.map((t) => (
                <tr key={t.trabalhadorId} className="border-t">
                  <td className="p-2 text-center">{t.nome}</td>
                  <td className="p-2 text-center">{new Date(t.dataCadastro).toLocaleDateString()}</td>
                  <td className="p-2 text-center">{t.dataUltimaAtualizacao ? new Date(t.dataUltimaAtualizacao).toLocaleDateString() : '-'}</td>
                  <td className="p-2 text-center">{t.statusCadastro}</td>
                  <td className="text-center">
                  <div className="btn-group gap-2">
                    <Link href={`/trabalhador/${t.trabalhadorId}/view`}>
                      <button className="btn btn-sm btn-outline-primary cursor-pointer" title="Ver">
                        <i className="bi bi-eye"></i>
                      </button>
                    </Link>
                    <Link href={`/trabalhador/${t.trabalhadorId}/edit`}>
                      <button className="btn btn-sm btn-outline-secondary cursor-pointer" title="Editar">
                        <i className="bi bi-pencil"></i>
                      </button>
                    </Link>
                  </div>
                </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  )
}