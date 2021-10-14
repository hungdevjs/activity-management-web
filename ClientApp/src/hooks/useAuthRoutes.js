import { useEffect } from "react"
import { useHistory } from "react-router-dom"
import { getAccessToken } from "../utils/helpers"

const useAuthRoutes = () => {
  const history = useHistory()

  useEffect(() => {
    const token = getAccessToken()
    if (token) history.push("/")
  }, [])
}

export default useAuthRoutes
