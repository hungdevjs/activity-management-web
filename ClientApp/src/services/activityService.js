import { api } from "./api"

const ACTIVITY_URL = `/activities`

export const getActivities = () => api.get(ACTIVITY_URL)

export const getActiveActivities = () => api.get(`${ACTIVITY_URL}/active`)

export const signUpActivity = (id) => api.post(`${ACTIVITY_URL}/${id}`)

export const attendanceActivity = (id) => api.put(`${ACTIVITY_URL}/${id}`)

export const getScore = (id) => api.put(`${ACTIVITY_URL}/score`)