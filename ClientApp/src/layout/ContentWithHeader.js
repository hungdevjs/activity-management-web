import React from "react"
import { Container } from "reactstrap"
import Header from "./Header"

const ContentWithHeader = ({ children }) => {
  return (
    <div className="vh-100 d-flex flex-column">
      <Header />
      <Container className="flex-grow-1">{children}</Container>
    </div>
  )
}

export default ContentWithHeader
