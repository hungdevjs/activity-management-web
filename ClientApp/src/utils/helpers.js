import { ACCESS_TOKEN } from "./constants"

export const getAccessToken = () => localStorage.getItem(ACCESS_TOKEN)

export const isCurrentSemester = (semester) => {
  const now = Date.now()
  const startTime = new Date(semester.startTime)
  const endTime = new Date(semester.endTime)

  return startTime <= now && now <= endTime
}
