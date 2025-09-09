import React, { useState } from 'react'
import { useRouter } from 'next/router'
import { useAuth } from '../../context/AuthProvider'
import { InputPassword } from '../components/Auth/InputPassword'

export default function LoginPage() {
  const { login } = useAuth()
  const router = useRouter()
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const handle = async (e: React.FormEvent) => {
    e.preventDefault()
    setError('')
    try {
      const user = await login(username, password)
      if (user?.roles?.includes('admin')) router.push('/dashboard?role=admin')
      else router.push('/dashboard')
    } catch (err: any) {
      setError(err?.response?.data?.message || 'Erro ao logar')
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center">
      <form onSubmit={handle} className="p-8 bg-white rounded shadow w-96">
        <h2 className="text-xl mb-4">Login</h2>
        {error && <div className="text-red-600 mb-2">{error}</div>}
        <label className="block mb-2">Usuário</label>
        <input value={username} onChange={(e) => setUsername(e.target.value)} className="border rounded px-3 py-2 w-full mb-3" />
        <label className="block mb-2">Senha</label>
        <InputPassword value={password} onChange={(e) => setPassword(e.target.value)} />
        <button className="mt-4 w-full py-2 rounded bg-blue-600 text-white">Entrar</button>
      </form>
    </div>
  )
}