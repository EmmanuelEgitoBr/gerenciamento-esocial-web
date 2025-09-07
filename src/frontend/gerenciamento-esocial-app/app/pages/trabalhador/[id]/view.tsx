import React, { useEffect, useState } from 'react'
import { useRouter } from 'next/router'
import { NavBar } from '../../../components/NavBar/NavBar'
import { getTrabalhadorById, getDependentesByTrabalhador, downloadPdfFile, uploadFileUrl } from '../../../lib/api'

export default function ViewTrabalhador() {
  const router = useRouter()
  const { id } = router.query
  const [trabalhador, setTrabalhador] = useState<any>(null)
  const [dependentes, setDependentes] = useState<any[]>([])

  useEffect(() => { if (!id) return; fetchData(Number(id)) }, [id])

  const fetchData = async (idNum: number) => {
    const res = await getTrabalhadorById(idNum)
    setTrabalhador(res.data)
    const d = await getDependentesByTrabalhador(idNum)
    setDependentes(d.data || [])
  }

  if (!trabalhador) return <div>Carregando...</div>

  return (
    <div>
      <NavBar />
      <div className="p-6">
        <h1 className="text-2xl">{trabalhador.nome}</h1>
        <div>Nascimento: {trabalhador.dataNascimento ?? '-'}</div>

        <div className="mt-4">
          <a href={downloadPdfFile(trabalhador.trabalhadorId)} target="_blank" rel="noreferrer" className="px-3 py-2 bg-blue-600 text-white rounded">Download PDF</a>
        </div>

        <h2 className="mt-6">Dependentes</h2>
        {dependentes.length === 0 && <div>Sem dependentes</div>}
        {dependentes.length > 0 && (
          <table className="w-full">
            <thead><tr><th>Nome</th><th>Data Nasc</th><th>Ação</th></tr></thead>
            <tbody>
              {dependentes.map(d => (
                <tr key={d.dependenteId}><td>{d.nomeDependente}</td><td>{d.dataNascimento}</td><td className="underline">Ver</td></tr>
              ))}
            </tbody>
          </table>
        )}

        <h2 className="mt-6">Upload de Arquivo</h2>
        <input type="file" onChange={async e => {
          const f = e.target.files?.[0]
          if (!f) return
          const fd = new FormData()
          fd.append('file', f)
          await fetch(uploadFileUrl(trabalhador.trabalhadorId), { method: 'POST', body: fd, credentials: 'include' })
          alert('Upload enviado')
        }} />
      </div>
    </div>
  )
}