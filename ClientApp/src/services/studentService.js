import { api } from "./api"

const STUDENT_URL = `/students`

export const getProfile = () => api.get(`${STUDENT_URL}/me`)

export const updateProfile = (data) => api.get(`${STUDENT_URL}/me`, data)
