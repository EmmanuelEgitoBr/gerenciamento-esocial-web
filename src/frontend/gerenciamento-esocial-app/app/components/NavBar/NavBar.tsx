import React from 'react'
import Link from 'next/link'
import Image from "next/image";
import { useAuth } from '../../../context/AuthProvider'

export const NavBar: React.FC = () => {
  const { user, logout } = useAuth()
  const isAdmin = user?.roles?.includes('Admin')

  return (
    <nav className="bg-white shadow p-4 flex justify-between items-center">
      <div className="flex items-center gap-4">
        <Link href="/">
          <Image
            src="/assets/images/logo.png"
            alt="Censo ESocial"
            width={75} 
            height={25}
          />
        </Link>
        {isAdmin && <Link href="/dashboard">Gerenciamento de cadastros</Link>}
        {isAdmin && <Link href="/usuarios">Criar usuários</Link>}
        {isAdmin && <Link href="/permissoes">Criar permissões</Link>}
      </div>
      <div>
        <button onClick={() => logout()} className="text-sm cursor-pointer">Logoff</button>
      </div>
    </nav>
  )
}