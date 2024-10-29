import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import {
  Avatar,
  Box,
  Button,
  Container,
  CssBaseline,
  Grid,
  Link,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import axios from "axios";
import React, { useState } from "react";
import { useDispatch } from "react-redux";
import * as Yup from "yup";
import {
  loginFailure,
  loginRequest,
  loginSuccess,
} from "../features/authSlice";
import { useNavigate } from "react-router-dom";

interface FormValues {
  userName: string;
  password: string;
}

const validationSchema = Yup.object().shape({
  userName: Yup.string().required("El usuario es obligatorio"),
  password: Yup.string()
    .min(6, "La contraseña debe tener al menos 6 caracteres")
    .required("La contraseña es obligatoria"),
});

const AuthComponent: React.FC = () => {
  const [userName, setUserName] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [errors, setErrors] = useState<
    Partial<Record<keyof FormValues, string>>
  >({});
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      await validationSchema.validate(
        { userName, password },
        { abortEarly: false }
      );
      dispatch(loginRequest());

      const response = await axios.post(
        "http://localhost:5231/api/account/login",
        {
          userName,
          password,
        }
      );

      const { token } = response.data;
      localStorage.setItem("authToken", token);
      dispatch(loginSuccess({ userName, token }));
      navigate("/dashboard");
    } catch (error: any) {
      if (error instanceof Yup.ValidationError) {
        const validationErrors: Record<string, string> = {};
        error.inner.forEach((err) => {
          if (err.path) {
            validationErrors[err.path] = err.message;
          }
        });
        setErrors(validationErrors);
      } else {
        dispatch(
          loginFailure(error.response?.data.message || "Error de autenticación")
        );
      }
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Paper
        elevation={3}
        sx={{
          marginTop: 8,
          padding: 4,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Iniciar sesión
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="userName"
            label="Usuario"
            name="userName"
            autoComplete="userName"
            autoFocus
            value={userName}
            onChange={(e) => {
              setUserName(e.target.value);
              setErrors((prev) => ({ ...prev, userName: undefined }));
            }}
            error={Boolean(errors.userName)}
            helperText={errors.userName}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Contraseña"
            type="password"
            id="password"
            autoComplete="current-password"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
              setErrors((prev) => ({ ...prev, password: undefined }));
            }}
            error={Boolean(errors.password)}
            helperText={errors.password}
          />

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Iniciar sesión
          </Button>
          <Grid container>
            <Grid item xs>
              <Link href="#" variant="body2">
                ¿Olvidaste tu contraseña?
              </Link>
            </Grid>
            <Grid item>
              <Link href="#" variant="body2">
                {"¿No tienes una cuenta? Regístrate"}
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Paper>
    </Container>
  );
};

export default AuthComponent;
