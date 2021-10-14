import React, { useContext } from "react"

import { AppContext } from "../contexts/app.context"

const Loading = () => {
  const { loading } = useContext(AppContext)
  if (!loading) return null

  return (
    <div className="d-flex align-items-center justify-content-center loading">
      <div className="spinner-border" role="status" />
    </div>
  )
}

export default Loading
