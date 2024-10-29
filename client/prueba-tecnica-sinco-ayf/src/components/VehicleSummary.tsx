import React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Container,
} from "@mui/material";

interface VehicleSummaryData {
  model: string;
  totalValue: number;
  vehicleCount: number;
}

// Añade summaryData como prop
const VehicleSummary: React.FC<{ summaryData: VehicleSummaryData[] }> = ({
  summaryData,
}) => {
  if (summaryData.length === 0) {
    return <Typography variant="h6">No hay datos disponibles</Typography>;
  }

  return (
    <Container maxWidth="xl">
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Modelo</TableCell>
              <TableCell>Valor Total</TableCell>
              <TableCell>Cantidad de Vehículos</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {summaryData.map((vehicle) => (
              <TableRow key={vehicle.model}>
                <TableCell>{vehicle.model}</TableCell>
                <TableCell>${vehicle.totalValue.toLocaleString()}</TableCell>
                <TableCell>{vehicle.vehicleCount}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default VehicleSummary;
