import axios from 'axios'

const API = process.env.NEXT_PUBLIC_API_BASE
export const api = axios.create({ baseURL: API, withCredentials: true })

export const downloadFile = (id: number) => `${API}/arquivos/download/${id}`
export const downloadPdfFile = (id: number) => `${API}/arquivos/download-pdf/${id}`
export const uploadFileUrl = (trabalhadorId: number) => `${API}/arquivos/${trabalhadorId}/upload`

export const getUsuarios = (page = 1, pageSize = 10) => api.get(`/auth/users?page=${page}&pageSize=${pageSize}`)
export const createUsuario = (payload: any) => api.post(`/auth/register`, payload)
export const getPermissoes = (page = 1, pageSize = 10) => api.get(`/auth/roles`)
export const addUsuarioToRole = (role: string, email: string) => api.post(
    `/auth/add-user-to-role?roleName=${role}`, 
    JSON.stringify(email),
    { headers: { "Content-Type": "application/json" } })

export const getTrabalhadores = (page = 1, pageSize = 10) => api.get(`/trabalhadores?page=${page}&pageSize=${pageSize}`)
export const getTrabalhadorByUserId = (id: string) => api.get(`/trabalhadores/user/${id}`)
export const getTrabalhadorById = (id: number) => api.get(`/trabalhadores/${id}`)
export const createTrabalhador = (payload: any) => api.post(`/trabalhadores/create`, payload)
export const updateTrabalhador = (payload: any) => api.put(`/trabalhadores/update`, payload)
export const atualizarStatus = (id: number, status: number) => api.put(`/trabalhadores/${id}/cadastro/atualizar-status`, { status })

export const getCedidosByTrabalhador = (id: number) => api.get(`/cedidos/trabalhador/${id}`)
export const createCedido = (payload: any) => api.post('/cedidos/create', payload)
export const getDependentesByTrabalhador = (id: number) => api.get(`/dependentes/trabalhador/${id}`)
export const createDependente = (payload: any) => api.post('/dependentes/create', payload)
export const getEstagiariosByTrabalhador = (id: number) => api.get(`/estagiarios/trabalhador/${id}`)
export const createEstagiario = (payload: any) => api.post('/estagiarios/create', payload)