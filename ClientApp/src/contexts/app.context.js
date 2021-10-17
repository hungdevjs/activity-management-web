import React, { useState, useMemo } from "react"

export const AppContext = React.createContext()

const AppContextProvider = ({ children }) => {
  const [loading, setLoading] = useState(false)
  const [user, setUser] = useState(null)
  const [semesters, setSemesters] = useState([])
  const [semesterId, setSemesterId] = useState(null)

  const selectedSemester = useMemo(() => {
    if (!semesters || !semesterId) return null
    return semesters.find((item) => item.id === semesterId) || null
  }, [semesters, semesterId])

  return (
    <AppContext.Provider
      value={{
        loading,
        user,
        semesters,
        semesterId,
        selectedSemester,
        setLoading,
        setUser,
        setSemesters,
        setSemesterId,
      }}
    >
      {children}
    </AppContext.Provider>
  )
}

export default AppContextProvider
