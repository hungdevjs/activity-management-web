import { ACCESS_TOKEN } from "./constants"

export const getAccessToken = () => localStorage.getItem(ACCESS_TOKEN)
