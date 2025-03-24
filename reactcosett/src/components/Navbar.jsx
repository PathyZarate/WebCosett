import { Link } from "react-router-dom";
import "./Navbar.css";

const Navbar = () => {
  return (
    <header>
      <div id="logo">
        <img src="https://cosett.bo/wp-content/uploads/2025/03/logoCosett2.png" alt="Logo Cosett" />
      </div>
      <nav>
        <ul>
          <li><Link to="/">Home</Link></li>
          <li className="separator">|</li>
          <li><Link to="/convertir">Convertir</Link></li>
          <li className="separator">|</li>
          <li><Link to="/concatenar">Concatenar</Link></li>
          <li className="separator">|</li>
          <li><Link to="/excel">Excel</Link></li>
        </ul>
      </nav>
    </header>
  );
};

export default Navbar;
