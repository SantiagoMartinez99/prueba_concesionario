import {
  Add as AddIcon,
  Delete as DeleteIcon,
  Edit as EditIcon,
  Sell as SellIcon,
} from "@mui/icons-material";
import {
  Box,
  Button,
  Container,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  Grid,
  IconButton,
  InputLabel,
  MenuItem,
  Paper,
  Select,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import React, { useEffect, useState } from "react";

interface Vehicle {
  id: number;
  model: string;
  color: string;
  mileage: number;
  price: number;
  image: string;
  registrationDate: Date;
  isUsed: boolean;
}

interface Motorcycle extends Vehicle {
  engineCapacity: number;
  gears: number;
}

interface VehicleDashboardProps {
  vehicleType: "car" | "motorcycle";
}

const VehicleDashboard: React.FC<VehicleDashboardProps> = ({ vehicleType }) => {
  const [vehicles, setVehicles] = useState<Vehicle[]>([]);
  const [open, setOpen] = useState(false);
  const [currentVehicle, setCurrentVehicle] = useState<Vehicle | null>(null);
  const [isUsed, setIsUsed] = useState<string>("nuevo");

  // Estado para manejar la venta
  const [sellOpen, setSellOpen] = useState(false);
  const [customerName, setCustomerName] = useState("");
  const [customerDocument, setCustomerDocument] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          `http://localhost:5231/api/${vehicleType}`
        );
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        const data: Vehicle[] = await response.json();
        setVehicles(data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, [vehicleType]);

  const handleOpen = (vehicle: Vehicle | null = null) => {
    setCurrentVehicle(vehicle);
    setIsUsed(vehicle && vehicle.isUsed ? "usado" : "nuevo");
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setCurrentVehicle(null);
    setIsUsed("nuevo");
  };

  const handleSave = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const newVehicle: Vehicle | Motorcycle = {
      id: currentVehicle ? currentVehicle.id : Date.now(),
      model: formData.get("model") as string,
      color: formData.get("color") as string,
      mileage: parseInt(formData.get("mileage") as string),
      price: parseFloat(formData.get("price") as string),
      image: formData.get("image") as string,
      registrationDate: new Date(),
      isUsed: isUsed === "usado",
    };

    // Agregar propiedades específicas para motocicletas
    if (vehicleType === "motorcycle") {
      (newVehicle as Motorcycle).engineCapacity = parseInt(
        formData.get("engineCapacity") as string
      );
      (newVehicle as Motorcycle).gears = parseInt(
        formData.get("gears") as string
      );
    }

    // Validaciones
    const validateVehicle = (): boolean => {
      const carLimit = 10;
      const motorcycleLimit = 15;
      const maxPrice = 250000000;
      const maxEngineCapacity = 400;

      const existingVehicle = vehicles.find(
        (v) => v.model === newVehicle.model && !v.isUsed
      );

      if (existingVehicle && newVehicle.price < existingVehicle.price * 0.85) {
        alert(
          "El precio del nuevo vehículo no puede ser inferior al 85% del precio del primero registrado con el mismo modelo."
        );
        return false;
      }

      const carCount = vehicles.filter((v) => !("engineCapacity" in v)).length;
      const motorcycleCount = vehicles.filter(
        (v) => "engineCapacity" in v
      ).length;

      if (vehicleType === "car" && carCount >= carLimit) {
        alert("No se pueden registrar más de 10 coches.");
        return false;
      }
      if (vehicleType === "motorcycle" && motorcycleCount >= motorcycleLimit) {
        alert("No se pueden registrar más de 15 motos.");
        return false;
      }
      if (newVehicle.price > maxPrice) {
        alert(
          `El precio del vehículo no puede ser mayor a ${maxPrice.toLocaleString()}.`
        );
        return false;
      }
      if (
        vehicleType === "motorcycle" &&
        (newVehicle as Motorcycle).engineCapacity > maxEngineCapacity
      ) {
        alert("El cilindraje de la moto debe ser igual o menor a 400 cc.");
        return false;
      }
      return true;
    };

    if (!validateVehicle()) return;

    const endpoint = currentVehicle
      ? `http://localhost:5231/api/${vehicleType}/${currentVehicle.id}`
      : `http://localhost:5231/api/${vehicleType}`;
    const method = currentVehicle ? "PUT" : "POST";

    await fetch(endpoint, {
      method,
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newVehicle),
    });

    setVehicles(
      currentVehicle
        ? vehicles.map((vehicle) =>
            vehicle.id === currentVehicle.id ? newVehicle : vehicle
          )
        : [...vehicles, newVehicle]
    );

    handleClose();
  };

  const handleDelete = async (id: number) => {
    await fetch(`http://localhost:5231/api/${vehicleType}/${id}`, {
      method: "DELETE",
    });
    setVehicles(vehicles.filter((vehicle) => vehicle.id !== id));
  };

  const handleSellOpen = (vehicle: Vehicle) => {
    setCurrentVehicle(vehicle);
    setSellOpen(true);
  };

  const handleSellClose = () => {
    setSellOpen(false);
    setCurrentVehicle(null);
    setCustomerName("");
    setCustomerDocument("");
  };

  const handleSell = async () => {
    if (!currentVehicle) return;

    // Crear la persona en la base de datos
    const buyerData = {
      Name: customerName,
      DocumentNumber: customerDocument,
      VehicleId: currentVehicle.id,
    };

    const customerResponse = await fetch("http://localhost:5231/api/customer", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(buyerData),
    });

    if (!customerResponse.ok) {
      console.error("Error creando cliente:", await customerResponse.text());
      return;
    }

    const saleData = { vehicleId: currentVehicle.id, buyer: buyerData };
    const saleResponse = await fetch("http://localhost:5231/api/sale", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(saleData),
    });

    if (!saleResponse.ok) {
      console.error("Error creando venta:", await saleResponse.text());
      return;
    }

    await fetch(
      `http://localhost:5231/api/${vehicleType}/${currentVehicle.id}`,
      {
        method: "DELETE",
      }
    );

    setVehicles(vehicles.filter((vehicle) => vehicle.id !== currentVehicle.id));

    handleSellClose();
  };

  return (
    <>
      <Container maxWidth="xl">
        <Box sx={{ my: 10 }}>
          <Typography variant="h4" component="h1" gutterBottom>
            DASHBOARD CONCESIONARIO AYF -{" "}
            {vehicleType === "car" ? "Coches" : "Motos"}
          </Typography>
          <Grid container spacing={2} alignItems="center" sx={{ mb: 2 }}>
            <Grid item xs={12} sm="auto">
              <Button
                variant="contained"
                startIcon={<AddIcon />}
                onClick={() => handleOpen()}
              >
                Añadir {vehicleType === "car" ? "Vehículo" : "Moto"}
              </Button>
            </Grid>
          </Grid>
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell>Modelo</TableCell>
                  <TableCell>Color</TableCell>
                  <TableCell>Precio</TableCell>
                  <TableCell>Kilometraje</TableCell>
                  <TableCell>Imagen</TableCell>
                  <TableCell>Fecha de Registro</TableCell>
                  <TableCell>Nuevo/Usado</TableCell>
                  {vehicleType === "motorcycle" && (
                    <TableCell>Cilindraje</TableCell>
                  )}
                  {vehicleType === "motorcycle" && (
                    <TableCell>Marchas</TableCell>
                  )}
                  <TableCell>Acciones</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {vehicles.map((vehicle) => (
                  <TableRow key={vehicle.id}>
                    <TableCell>{vehicle.model}</TableCell>
                    <TableCell>{vehicle.color}</TableCell>
                    <TableCell>{vehicle.price}</TableCell>
                    <TableCell>{vehicle.mileage}</TableCell>
                    <TableCell>
                      <img
                        src={vehicle.image}
                        alt={vehicle.model}
                        style={{ width: "50px", height: "50px" }}
                      />
                    </TableCell>
                    <TableCell>
                      {new Date(vehicle.registrationDate).toLocaleDateString()}
                    </TableCell>
                    <TableCell>{vehicle.isUsed ? "Usado" : "Nuevo"}</TableCell>
                    {vehicleType === "motorcycle" && (
                      <TableCell>
                        {(vehicle as Motorcycle).engineCapacity}
                      </TableCell>
                    )}
                    {vehicleType === "motorcycle" && (
                      <TableCell>{(vehicle as Motorcycle).gears}</TableCell>
                    )}
                    <TableCell>
                      <IconButton
                        color="primary"
                        onClick={() => handleOpen(vehicle)}
                      >
                        <EditIcon />
                      </IconButton>
                      <IconButton
                        color="error"
                        onClick={() => handleDelete(vehicle.id)}
                      >
                        <DeleteIcon />
                      </IconButton>
                      <IconButton
                        color="success"
                        onClick={() => handleSellOpen(vehicle)}
                      >
                        <SellIcon />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Box>
      </Container>

      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>
          {currentVehicle ? "Editar Vehículo" : "Agregar Vehículo"}
        </DialogTitle>
        <DialogContent>
          <form onSubmit={handleSave}>
            <TextField
              label="Modelo"
              name="model"
              defaultValue={currentVehicle ? currentVehicle.model : ""}
              fullWidth
              required
              sx={{ my: 1 }}
            />
            <TextField
              label="Color"
              name="color"
              defaultValue={currentVehicle ? currentVehicle.color : ""}
              fullWidth
              required
              sx={{ my: 1 }}
            />
            <TextField
              label="Kilometraje"
              name="mileage"
              type="number"
              defaultValue={currentVehicle ? currentVehicle.mileage : 0}
              fullWidth
              required
              sx={{ my: 1 }}
            />
            <TextField
              label="Precio"
              name="price"
              type="number"
              defaultValue={currentVehicle ? currentVehicle.price : 0}
              fullWidth
              required
              sx={{ my: 1 }}
            />
            <TextField
              label="Imagen URL"
              name="image"
              defaultValue={currentVehicle ? currentVehicle.image : ""}
              fullWidth
              required
              sx={{ my: 1 }}
            />
            {vehicleType === "motorcycle" && (
              <>
                <TextField
                  label="Cilindraje"
                  name="engineCapacity"
                  type="number"
                  defaultValue={
                    currentVehicle
                      ? (currentVehicle as Motorcycle).engineCapacity
                      : 0
                  }
                  fullWidth
                  required
                  sx={{ my: 1 }}
                />
                <TextField
                  label="Marchas"
                  name="gears"
                  type="number"
                  defaultValue={
                    currentVehicle ? (currentVehicle as Motorcycle).gears : 0
                  }
                  fullWidth
                  required
                  sx={{ my: 1 }}
                />
              </>
            )}
            <FormControl fullWidth>
              <InputLabel id="isUsed-label">Estado</InputLabel>
              <Select
                labelId="isUsed-label"
                value={isUsed}
                onChange={(e) => setIsUsed(e.target.value)}
                required
                sx={{ my: 1 }}
              >
                <MenuItem value="nuevo">Nuevo</MenuItem>
                <MenuItem value="usado">Usado</MenuItem>
              </Select>
            </FormControl>
            <DialogActions>
              <Button onClick={handleClose}>Cancelar</Button>
              <Button type="submit">
                {currentVehicle ? "Actualizar" : "Agregar"}
              </Button>
            </DialogActions>
          </form>
        </DialogContent>
      </Dialog>

      <Dialog open={sellOpen} onClose={handleSellClose}>
        <DialogTitle>Vender Vehículo</DialogTitle>
        <DialogContent>
          <TextField
            label="Nombre del Cliente"
            value={customerName}
            onChange={(e) => setCustomerName(e.target.value)}
            fullWidth
            required
            sx={{ my: 1 }}
          />
          <TextField
            label="Número de Documento"
            value={customerDocument}
            onChange={(e) => setCustomerDocument(e.target.value)}
            fullWidth
            required
            sx={{ my: 1 }}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleSellClose}>Cancelar</Button>
          <Button onClick={handleSell}>Vender</Button>
        </DialogActions>
      </Dialog>
    </>
  );
};

export default VehicleDashboard;
