// src/store/vehicleSummarySlice.ts
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

export interface VehicleSummaryData {
  model: string;
  totalValue: number;
  vehicleCount: number;
  isSold: boolean; // Asegúrate de que esto esté en tus datos
}

interface VehicleSummaryState {
  summaryData: VehicleSummaryData[];
  loading: boolean;
  error: string | null;
}

const initialState: VehicleSummaryState = {
  summaryData: [],
  loading: false,
  error: null,
};

// Async thunk for fetching vehicle summary data
export const fetchSummaryData = createAsyncThunk(
  "vehicles/fetchSummaryData",
  async () => {
    const response = await axios.get(
      "http://localhost:5231/api/vehiclesummary"
    );
    return response.data;
  }
);

const vehicleSummarySlice = createSlice({
  name: "vehicleSummary",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchSummaryData.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSummaryData.fulfilled, (state, action) => {
        state.loading = false;
        state.summaryData = action.payload.filter(
          (vehicle: VehicleSummaryData) => !vehicle.isSold
        );
      })
      .addCase(fetchSummaryData.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || "An unknown error occurred";
      });
  },
});

export default vehicleSummarySlice.reducer;
