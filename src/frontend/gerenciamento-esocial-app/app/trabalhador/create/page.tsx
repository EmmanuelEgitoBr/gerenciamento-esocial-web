"use client";

import React, { useState } from 'react'
import { NavBar } from '../../components/NavBar/NavBar'
import { createTrabalhador } from '../../lib/api'
import { useRouter } from 'next/navigation'

export default function CreateTrabalhador() {
  const router = useRouter()
  const [form, setForm] = useState<any>({ nome: '', tipo: 'CLT' })
  const [saving, setSaving] = useState(false)

  const handleSave = async () => {
    setSaving(true)
    try {
      await createTrabalhador(form)
      router.push('/dashboard')
    } catch (e) { console.error(e) }
    setSaving(false)
  }

  return (
    <div>
      <NavBar />
      <div className="p-6 max-w-3xl">
        <h2 className="text-xl mb-4">Novo Trabalhador</h2>
        <label className="block">Nome</label>
        <input value={form.nome} onChange={(e) => setForm({ ...form, nome: e.target.value })} className="border w-full p-2 mb-3" />

        <label className="block">Tipo</label>
        <select value={form.tipo} onChange={(e) => setForm({ ...form, tipo: e.target.value })} className="border w-full p-2 mb-3">
          <option>CLT</option>
          <option>Estagiario</option>
          <option>Outro</option>
        </select>

        <div className="flex gap-3 mt-4">
          <button onClick={handleSave} className="px-4 py-2 bg-green-600 text-white" disabled={saving}>Finalizar cadastro</button>
        </div>
      </div>
    </div>
  )
}