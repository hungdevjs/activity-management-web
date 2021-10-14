import { api } from "./api"

const ACCOUNT_URL = `/accounts`

export const logIn = (data) => api.post(`${ACCOUNT_URL}/login`, data)

export const getUserInfo = () => api.get(`${ACCOUNT_URL}/me`)
