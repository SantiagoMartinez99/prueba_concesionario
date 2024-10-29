import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import axios from "axios";

interface AuthState {
  userName: string | null;
  token: string | null;
  isAuthenticated: boolean;
}

const initialState: AuthState = {
  userName: null,
  token: localStorage.getItem("authToken"),
  isAuthenticated: !!localStorage.getItem("authToken"),
};

export const checkAuthStatus = createAsyncThunk(
  "auth/checkAuthStatus",
  async (_: any, thunkAPI: { rejectWithValue: (arg0: string) => any }) => {
    try {
      const token = localStorage.getItem("authToken");
      if (token) {
        const response = await axios.get("/api/auth/verify", {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (response.data.isAuthenticated) {
          return { isAuthenticated: true, userName: response.data.userName };
        }
      }
      return { isAuthenticated: false, userName: null };
    } catch (error) {
      return thunkAPI.rejectWithValue("Error al verificar autenticación");
    }
  }
);

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginRequest: (state) => {
      state.isAuthenticated = false;
    },
    loginSuccess: (
      state,
      action: PayloadAction<{ userName: string; token: string }>
    ) => {
      state.userName = action.payload.userName;
      state.token = action.payload.token;
      state.isAuthenticated = true;
    },
    loginFailure: (state) => {
      state.userName = null;
      state.token = null;
      state.isAuthenticated = false;
    },
    logout: (state) => {
      state.userName = null;
      state.token = null;
      state.isAuthenticated = false;
      localStorage.removeItem("authToken");
    },
  },
});

export const { loginRequest, loginSuccess, loginFailure, logout } =
  authSlice.actions;
export default authSlice.reducer;
