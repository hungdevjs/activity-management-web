import Dashboard from "../pages/Dashboard"
import Activity from "../pages/Activity"
import Profile from "../pages/Profile"

const routes = [
  {
    name: "Dashboard",
    path: "/",
    component: Dashboard,
    exact: true,
  },
  {
    name: "Activity",
    path: "/activity",
    component: Activity,
    exact: true,
  },
  {
    name: "Profile",
    path: "/profile",
    component: Profile,
    exact: true,
  },
]

export default routes
