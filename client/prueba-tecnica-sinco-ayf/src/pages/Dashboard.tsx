import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import Navbar from "../components/Navbar";
import VehicleDashboard from "../components/VehicleDashboard";
import VehicleSummary from "../components/VehicleSummary";
import { fetchSummaryData } from "../features/summarySlice";
import { AppDispatch } from "../features/store";
import { Typography } from "@mui/material";

interface RootState {
  vehicleSummary: {
    summaryData: [];
    loading: boolean;
    error: string | null;
  };
}

export default function Component() {
  const dispatch = useDispatch<AppDispatch>();

  const { summaryData, loading, error } = useSelector(
    (state: RootState) => state.vehicleSummary
  );

  useEffect(() => {
    dispatch(fetchSummaryData());
  }, [dispatch]);


  return (
    <>
      <Navbar />
      {loading && <Typography variant="h6">Cargando datos...</Typography>}
      {error && (
        <Typography variant="h6" color="error">
          Error: {error}
        </Typography>
      )}
      <VehicleDashboard vehicleType="car" />
      <VehicleDashboard vehicleType="motorcycle" />
      <VehicleSummary summaryData={summaryData} />
    </>
  );
}
