import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Convertir from "../pages/Convertir";
import Concatenar from "../pages/Concatenar";
import Excel from "../pages/Excel";
import Navbar from "../components/Navbar";

const AppRouter = () => {
    return (
        <Router>
            <Navbar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/convertir" element={<Convertir />} />
                <Route path="/concatenar" element={<Concatenar />} />
                <Route path="/excel" element={<Excel />} />
            </Routes>
        </Router>
    );
};

export default AppRouter;
