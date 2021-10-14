import React, { useState, useContext } from "react"
import { toast } from "react-toastify"
import {
  Collapse,
  Container,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from "reactstrap"
import { MdLogout } from "react-icons/md"
import { Link, useHistory } from "react-router-dom"

import { AppContext } from "../contexts/app.context"
import { ACCESS_TOKEN } from "../utils/constants"

const Header = () => {
  const history = useHistory()
  const { user } = useContext(AppContext)
  const [collapsed, setCollapsed] = useState(true)

  const toggleNavbar = () => setCollapsed(!collapsed)

  const logOut = () => {
    localStorage.removeItem(ACCESS_TOKEN)
    history.push("/login")
    toast.info("Log out successfully!")
  }

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        light
      >
        <Container>
          <NavbarBrand tag={Link} to="/">
            {user?.name}
          </NavbarBrand>
          <NavbarToggler onClick={toggleNavbar} className="mr-2" />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!collapsed}
            navbar
          >
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">
                  Dashboard
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/activity">
                  Activity
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/profile">
                  Profile
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink className="cursor-pointer">
                  <MdLogout size={24} onClick={logOut} />
                </NavLink>
              </NavItem>
            </ul>
          </Collapse>
        </Container>
      </Navbar>
    </header>
  )
}

export default Header
