import { api } from "./api"

const ACTIVITY_URL = `/activities`

export const getActivities = () => api.get(ACTIVITY_URL)

export const getActiveActivities = () => api.get(`${ACTIVITY_URL}/active`)

export const signUpActivity = (id) => api.post(`${ACTIVITY_URL}/${id}`)

export const attendanceActivity = (id, attendanceCode) =>
  api.put(`${ACTIVITY_URL}/${id}?attendanceCode=${attendanceCode}`)

export const getScore = () => api.get(`${ACTIVITY_URL}/score`)

export const getAllSemesters = () => api.get(`${ACTIVITY_URL}/semesters`)

export const updateStatusActivity = () => api.post(ACTIVITY_URL)
