import { api } from "./api"

const ACTIVITY_URL = `/activities`

export const getActivities = (semesterId) =>
  api.get(ACTIVITY_URL, { params: { semesterId } })

export const getActiveActivities = (semesterId) =>
  api.get(`${ACTIVITY_URL}/active`, { params: { semesterId } })

export const signUpActivity = (id) => api.post(`${ACTIVITY_URL}/${id}`)

export const attendanceActivity = (id, attendanceCode) =>
  api.put(`${ACTIVITY_URL}/${id}?attendanceCode=${attendanceCode}`)

export const getScore = (semesterId) =>
  api.get(`${ACTIVITY_URL}/score`, { params: { semesterId } })

export const getAllSemesters = () => api.get(`${ACTIVITY_URL}/semesters`)

export const updateStatusActivity = () => api.post(ACTIVITY_URL)
