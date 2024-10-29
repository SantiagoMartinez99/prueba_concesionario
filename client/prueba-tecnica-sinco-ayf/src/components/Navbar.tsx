import MenuIcon from "@mui/icons-material/Menu";
import { InputLabel } from "@mui/material";
import AppBar from "@mui/material/AppBar";
import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Container from "@mui/material/Container";
import IconButton from "@mui/material/IconButton";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import Toolbar from "@mui/material/Toolbar";
import Tooltip from "@mui/material/Tooltip";
import Typography from "@mui/material/Typography";
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { checkAuthStatus, logout } from "../features/authSlice";
import { AppDispatch, RootState } from "../features/store";

function Navbar() {
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();
  const user = useSelector((state: RootState) => state.auth.userName);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkAuth = async () => {
      await dispatch(checkAuthStatus(user));
      setLoading(false);
    };
    checkAuth();
  }, [dispatch]);

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(
    null
  );

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };

  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleLogout = () => {
    dispatch(logout());
    navigate("/");
  };

  const settings = ["Home", "Dashboard", "Logout"];
  const handleMenuClick = (setting: string) => {
    const actions = {
      Home: () => navigate("/"),
      Dashboard: () => navigate("/dashboard"),
      Logout: handleLogout,
    };
    (actions[setting as keyof typeof actions] || handleCloseUserMenu)();
  };

  return (
    <>
      {loading ? (
        <div>Loading...</div>
      ) : (
        <AppBar
          position="relative"
          sx={{
            backgroundColor: "rgba(255, 255, 255, 0.1)",
            boxShadow: "none",
          }}
        >
          <Container maxWidth="xl">
            <Toolbar disableGutters>
              <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
                <Link to="/">
                  <img
                    src="https://www.sinco.co/hs-fs/hubfs/sinco-site/logos%202024/Logo%20-_Logo%20SINCO%20ERP%20Original.png?width=240&height=71&name=Logo%20-_Logo%20SINCO%20ERP%20Original.png"
                    alt="SINCO Logo"
                    style={{
                      marginRight: "8px",
                      marginTop: "8px",
                      height: "40px",
                      width: "auto",
                    }}
                  />
                </Link>
                <IconButton
                  size="large"
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  onClick={handleOpenNavMenu}
                  sx={{ color: "black" }}
                >
                  <MenuIcon />
                </IconButton>
              </Box>

              <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
                <Link to="/">
                  <img
                    src="https://www.sinco.co/hs-fs/hubfs/sinco-site/logos%202024/Logo%20-_Logo%20SINCO%20ERP%20Original.png?width=240&height=71&name=Logo%20-_Logo%20SINCO%20ERP%20Original.png"
                    alt="SINCO Logo"
                    style={{
                      marginRight: "8px",
                      marginTop: "20px",
                      height: "30px",
                    }}
                  />
                </Link>
              </Box>

              <Box sx={{ flexGrow: 0, display: { xs: "block", md: "none" } }}>
                <Link to="/login" style={{ textDecoration: "none" }}>
                  <Button variant="outlined" sx={{ color: "black" }}>
                    Login
                  </Button>
                </Link>
              </Box>

              <Box sx={{ flexGrow: 0, display: { xs: "none", md: "flex" } }}>
                {user ? (
                  <>
                    <Tooltip title="Open settings">
                      <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                        <Avatar>{user?.charAt(0)}</Avatar>
                      </IconButton>
                    </Tooltip>
                    <Menu
                      sx={{ mt: "45px" }}
                      id="menu-appbar"
                      anchorEl={anchorElUser}
                      anchorOrigin={{
                        vertical: "top",
                        horizontal: "right",
                      }}
                      keepMounted
                      transformOrigin={{
                        vertical: "top",
                        horizontal: "right",
                      }}
                      open={Boolean(anchorElUser)}
                      onClose={handleCloseUserMenu}
                    >
                      <MenuItem disabled>
                        <Typography sx={{ textAlign: "center" }}>
                          Bienvenido {user}!
                        </Typography>
                      </MenuItem>
                      {settings.map((setting) => (
                        <MenuItem
                          key={setting}
                          onClick={() => handleMenuClick(setting)}
                        >
                          <Typography sx={{ textAlign: "center" }}>
                            {setting}
                          </Typography>
                        </MenuItem>
                      ))}
                    </Menu>
                  </>
                ) : (
                  <Link to="/login" style={{ textDecoration: "none" }}>
                    <Button variant="outlined" sx={{ color: "black" }}>
                      Login
                    </Button>
                  </Link>
                )}
              </Box>
            </Toolbar>
          </Container>
        </AppBar>
      )}
    </>
  );
}

export default Navbar;
