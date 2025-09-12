"use client";

import React, { useState } from "react";

interface Dependente {
  tipoDependente: number;
  nomeDependente: string;
  dataNascimento: string;
  cpfDependente: string;
  ehDepTrabalhadorIrrf: boolean;
  ehDepIncapaFisMentTrab: boolean;
  ehDependentePensao: boolean;
  responsavel: string;
  telefoneResponsavel: string;
}

interface DependenteModalProps {
  open: boolean;
  onClose: () => void;
  onSave: (dependente: Dependente) => void;
}

export function DependenteModal({ open, onClose, onSave }: DependenteModalProps) {
  const [dependente, setDependente] = useState<Dependente>({
    tipoDependente: 0,
    nomeDependente: "",
    dataNascimento: "",
    cpfDependente: "",
    ehDepTrabalhadorIrrf: false,
    ehDepIncapaFisMentTrab: false,
    ehDependentePensao: false,
    responsavel: "",
    telefoneResponsavel: "",
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value, type } = e.currentTarget;
    const checked = (e.currentTarget as HTMLInputElement).checked;
    setDependente({
      ...dependente,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSave = () => {
    onSave(dependente);
    setDependente({
      tipoDependente: 0,
      nomeDependente: "",
      dataNascimento: "",
      cpfDependente: "",
      ehDepTrabalhadorIrrf: false,
      ehDepIncapaFisMentTrab: false,
      ehDependentePensao: false,
      responsavel: "",
      telefoneResponsavel: "",
    });
  };

  if (!open) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg p-6 w-96 shadow-lg max-h-[90vh] overflow-y-auto">
        <h3 className="text-lg font-semibold mb-4">Novo Dependente</h3>

        {/* TipoDependente */}
        <label className="block">Tipo Dependente</label>
        <select
          name="tipoDependente"
          value={dependente.tipoDependente}
          onChange={handleChange}
          className="border w-full p-2 mb-3"
        >
          <option value={0}>Selecione</option>
          <option value={1}>Filho(a)</option>
          <option value={2}>Cônjuge</option>
          <option value={3}>Outro</option>
        </select>

        {/* Nome */}
        <label className="block">Nome</label>
        <input
          name="nomeDependente"
          value={dependente.nomeDependente}
          onChange={handleChange}
          className="border w-full p-2 mb-3"
        />

        {/* Data Nascimento */}
        <label className="block">Data de Nascimento</label>
        <input
          type="date"
          name="dataNascimento"
          value={dependente.dataNascimento}
          onChange={handleChange}
          className="border w-full p-2 mb-3"
        />

        {/* CPF */}
        <label className="block">CPF</label>
        <input
          name="cpfDependente"
          value={dependente.cpfDependente}
          onChange={handleChange}
          className="border w-full p-2 mb-3"
        />

        {/* Checkboxes */}
        <label className="flex items-center gap-2">
          <input
            type="checkbox"
            name="ehDepTrabalhadorIrrf"
            checked={dependente.ehDepTrabalhadorIrrf}
            onChange={handleChange}
          />
          Dependente para IRRF
        </label>

        <label className="flex items-center gap-2">
          <input
            type="checkbox"
            name="ehDepIncapaFisMentTrab"
            checked={dependente.ehDepIncapaFisMentTrab}
            onChange={handleChange}
          />
          Dependente incapaz (físico/mental)
        </label>

        <label className="flex items-center gap-2 mb-3">
          <input
            type="checkbox"
            name="ehDependentePensao"
            checked={dependente.ehDependentePensao}
            onChange={handleChange}
          />
          Dependente de pensão
        </label>

        {/* Responsável */}
        <label className="block">Responsável</label>
        <input
          name="responsavel"
          value={dependente.responsavel}
          onChange={handleChange}
          className="border w-full p-2 mb-3"
        />

        {/* Telefone */}
        <label className="block">Telefone do Responsável</label>
        <input
          name="telefoneResponsavel"
          value={dependente.telefoneResponsavel}
          onChange={handleChange}
          className="border w-full p-2 mb-3"
        />

        <div className="flex justify-end gap-3 mt-4">
          <button onClick={onClose} className="px-3 py-1 bg-gray-300 rounded">
            Cancelar
          </button>
          <button
            onClick={handleSave}
            className="px-3 py-1 bg-blue-600 text-white rounded"
          >
            Adicionar
          </button>
        </div>
      </div>
    </div>
  );
}
