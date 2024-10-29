import { Box, Typography, Button, Container, Link } from "@mui/material";

const Hero = () => {
  return (
    <Box
      sx={{
        position: "relative",
        minHeight: "100vh",
        background: `linear-gradient(rgba(255, 255, 255, 0.7), rgba(255, 255, 255, 0.7)), url(https://images.unsplash.com/photo-1519641471654-76ce0107ad1b?fm=jpg&q=60&w=3000)`,
        backgroundSize: "cover",
        backgroundPosition: "center",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <Container maxWidth="md">
        <Box sx={{ textAlign: "center", color: "black", py: 5 }}>
          <Typography variant="h2" component="h1" gutterBottom>
            Bienvenido al concesionario AIF
          </Typography>
          <Typography sx={{ mb: 5 }} variant="h5" component="p" gutterBottom>
            Descubre el vehículo ideal al mejor precio. Explora nuestra amplia
            selección de modelos y encuentra el que se ajuste a tus necesidades.
            ¡Visítanos y haz realidad tu próximo automóvil!
          </Typography>
          <Link href="/dashboard">
            <Button variant="contained" color="primary" size="large">
              DASHBOARD
            </Button>
          </Link>
        </Box>
      </Container>
    </Box>
  );
};
export default Hero;
