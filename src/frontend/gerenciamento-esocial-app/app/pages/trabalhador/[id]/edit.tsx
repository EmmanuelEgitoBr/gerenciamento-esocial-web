import React, { useEffect, useState } from 'react'
import { useRouter } from 'next/router'
import { NavBar } from '../../../components/NavBar/NavBar'
import { getTrabalhadorById, updateTrabalhador } from '../../../lib/api'

export default function EditTrabalhador() {
  const router = useRouter()
  const { id } = router.query
  const [form, setForm] = useState<any>(null)

  useEffect(() => { if (!id) return; (async () => { const r = await getTrabalhadorById(Number(id)); setForm(r.data) })() }, [id])

  if (!form) return <div>Carregando...</div>

  return (
    <div>
      <NavBar />
      <div className="p-6 max-w-3xl">
        <h2 className="text-xl mb-4">Editar Trabalhador</h2>
        <label>Nome</label>
        <input value={form.nome} onChange={(e) => setForm({ ...form, nome: e.target.value })} className="border w-full p-2 mb-3" />
        <div className="flex gap-3">
          <button onClick={async () => { await updateTrabalhador(form); router.push('/dashboard') }} className="px-4 py-2 bg-blue-600 text-white">Salvar</button>
        </div>
      </div>
    </div>
  )
}