import React, { useState, useContext } from "react"

export const AppContext = React.createContext()

const AppContextProvider = ({ children }) => {
  const [loading, setLoading] = useState(false)
  const [user, setUser] = useState(null)

  return (
    <AppContext.Provider value={{ loading, user, setLoading, setUser }}>
      {children}
    </AppContext.Provider>
  )
}

export default AppContextProvider
