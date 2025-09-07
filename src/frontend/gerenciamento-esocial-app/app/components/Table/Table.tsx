import React from 'react'

export const Table: React.FC<{ columns: string[]; data: any[] }> = ({ columns, data }) => {
  return (
    <table className="min-w-full bg-white">
      <thead>
        <tr>{columns.map(c => <th key={c} className="p-2 text-left">{c}</th>)}</tr>
      </thead>
      <tbody>
        {data.map((row, i) => (
          <tr key={i} className="border-t">
            {columns.map((col) => <td className="p-2" key={col}>{row[col.toLowerCase().replace(/\s/g,'')] ?? ''}</td>)}
          </tr>
        ))}
      </tbody>
    </table>
  )
}