import { api } from "./api"

const ACCOUNT_URL = `/accounts`

export const logIn = (data) => api.post(`${ACCOUNT_URL}/login`, data)

export const getUserInfo = () => api.get(`${ACCOUNT_URL}/me`)

export const requestForgetPassword = (email) =>
  api.get(`${ACCOUNT_URL}/forget-password`, { params: { email } })

export const verifyForgetPassword = (data) =>
  api.post(`${ACCOUNT_URL}/verify-forget-password`, data)
