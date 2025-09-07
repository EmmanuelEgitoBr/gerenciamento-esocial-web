import React from 'react'
import Link from 'next/link'
import { useAuth } from '../../context/AuthContext'

export const NavBar: React.FC = () => {
  const { user, logout } = useAuth()
  const isAdmin = user?.roles?.includes('admin')

  return (
    <nav className="bg-white shadow p-4 flex justify-between">
      <div className="flex items-center gap-4">
        <div className="font-bold">Sistema</div>
        <Link href="/dashboard">Cadastro</Link>
        {isAdmin && <Link href="/gerenciamento">Gerenciamento de cadastros</Link>}
        {isAdmin && <Link href="/usuarios">Criar usuários</Link>}
        {isAdmin && <Link href="/permissoes">Criar permissões</Link>}
      </div>
      <div>
        <button onClick={() => logout()} className="text-sm">Logoff</button>
      </div>
    </nav>
  )
}