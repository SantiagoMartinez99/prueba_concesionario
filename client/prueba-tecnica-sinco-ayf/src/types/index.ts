// Representa un vehículo genérico
export interface Vehicle {
  type: any;
  id: number;
  model: string;
  color: string;
  mileage: number;
  price: number;
  image: string;
  registrationDate: Date;
  status: "available" | "sold";
  isUsed: boolean;
}

export interface Car extends Vehicle {}

export interface Motorcycle extends Vehicle {
  engineCapacity: number;
  gears: number;
}

export interface VehicleState {
  vehicles: Vehicle[];
  loading: boolean;
  error: string | null;
}

export interface CustomerInfo {
  customerName: string;
  customerDocument: string;
}
