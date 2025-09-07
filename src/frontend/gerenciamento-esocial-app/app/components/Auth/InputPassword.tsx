"use client";

import React, { useState } from 'react'

export const InputPassword: React.FC<React.InputHTMLAttributes<HTMLInputElement>> = (props) => {
  const [show, setShow] = useState(false)
  return (
    <div className="relative">
      <input {...props} type={show ? 'text' : 'password'} className="border rounded px-3 py-2 w-full" />
      <button type="button" onClick={() => setShow((s) => !s)} className="absolute right-2 top-2">{show ? '🙈' : '👁️'}</button>
    </div>
  )
}