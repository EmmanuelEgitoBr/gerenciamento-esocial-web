import React, { useEffect, useState } from 'react'
import { NavBar } from '../../components/NavBar/NavBar'
import { useAuth } from '../../context/AuthContext'
import { getTrabalhadores } from '../../lib/api'
import Link from 'next/link'

export default function Dashboard() {
  const { user } = useAuth()
  const [trabalhadores, setTrabalhadores] = useState<any[]>([])
  const [loading, setLoading] = useState(false)

  useEffect(() => { fetchList() }, [])

  const fetchList = async () => {
    setLoading(true)
    try {
      const res = await getTrabalhadores()
      setTrabalhadores(res.data || [])
    } catch (e) {
      setTrabalhadores([])
    }
    setLoading(false)
  }

  const hasCadastro = trabalhadores.length > 0

  return (
    <div>
      <NavBar />
      <div className="p-6">
        <div className="flex justify-between items-center mb-4">
          <h1 className="text-2xl">Cadastros</h1>
          <Link href="/trabalhador/create">
            <button className="px-4 py-2 rounded bg-blue-600 text-white" disabled={hasCadastro}>Novo cadastro</button>
          </Link>
        </div>

        {!hasCadastro && <div>Não há cadastro disponível... favor cadastrar</div>}

        {hasCadastro && (
          <table className="min-w-full bg-white">
            <thead>
              <tr>
                <th className="p-2 text-left">Nome</th>
                <th className="p-2 text-left">Data de Cadastro</th>
                <th className="p-2 text-left">Última Atualização</th>
                <th className="p-2 text-left">Status</th>
                <th className="p-2">Ações</th>
              </tr>
            </thead>
            <tbody>
              {trabalhadores.map((t) => (
                <tr key={t.trabalhadorId} className="border-t">
                  <td className="p-2">{t.nome}</td>
                  <td className="p-2">{new Date(t.dataCadastro).toLocaleDateString()}</td>
                  <td className="p-2">{t.dataUltimaAtualizacao ? new Date(t.dataUltimaAtualizacao).toLocaleDateString() : '-'}</td>
                  <td className="p-2">{t.statusCadastro}</td>
                  <td className="p-2">
                    <Link href={`/trabalhador/${t.trabalhadorId}/view`}><span className="mr-2 underline">Ver</span></Link>
                    <Link href={`/trabalhador/${t.trabalhadorId}/edit`}><span className="underline">Editar</span></Link>
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