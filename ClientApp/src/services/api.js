import axios from "axios"
import { BASE_URL } from "../utils/constants"
import { getAccessToken } from "../utils/helpers"

export const api = axios.create({
  baseURL: BASE_URL,
  timeout: 0,
  headers: {
    "Content-Type": "application/json",
  },
})

api.interceptors.request.use(
  (config) => {
    const token = getAccessToken()
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => Promise.reject(error)
)
