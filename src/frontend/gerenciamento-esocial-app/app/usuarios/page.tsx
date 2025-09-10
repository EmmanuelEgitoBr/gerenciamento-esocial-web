"use client";

import React, { useEffect, useState } from 'react'
import { NavBar } from '../components/NavBar/NavBar'
import { useAuth } from '../../context/AuthProvider'
import { getUsuarios } from '../lib/api'
import Link from 'next/link'

export default function Users() {
  const { user, loading } = useAuth()
  const [usuarios, setUsuarios] = useState<any[]>([])
  
  useEffect(() => {
    if (!loading) {
      fetchList() 
    }
  }, [loading])

  const fetchList = async () => {
    try {
      const res = await getUsuarios()
      setUsuarios(res.data.result || [])
    } catch (e) {
      setUsuarios([])
    }
  }

  const hasCadastro = usuarios.length > 0

  return (
    <div>
      <NavBar />
      <div className="p-6">
        <div className="flex justify-between items-center mb-4">
          <h1 className="text-2xl">Lista de Usuários Cadastrados</h1>
          <Link href="/usuarios/create">
              <button
                className="px-4 py-2 rounded bg-blue-600 text-white cursor-pointer"
              >
                Novo Usuário
              </button>
            </Link>
        </div>
        <br/>
        <hr/>

        {!hasCadastro && <div>Não há cadastro disponível</div>}

        {hasCadastro && (
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Email</th>
                <th className="p-2 text-center" style={{color:'#013A73'}}>Data de Criação</th>
              </tr>
            </thead>
            <tbody>
              {usuarios.map((u) => (
                <tr key={u.userId} className="border-t">
                  <td className="p-2 text-center">{u.email}</td>
                  <td className="p-2 text-center">{u.createdAt ? new Date(u.createdAt).toLocaleDateString() : '-'}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  )
}