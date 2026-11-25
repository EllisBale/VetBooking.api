import { BrowserRouter, Routes, Route } from "react-router-dom";
import Vet from "./pages/Vet";
import Layout from "./components/Layout";
import Owner from "./pages/Owner";
import Pet from "./pages/Pet";
import AppointmentsCalendar from "./pages/AppointmentsCalendar";

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/vets" element={<Vet />}></Route>
          <Route path="/owners" element={<Owner />}></Route>
          <Route path="/pets" element={<Pet />}></Route>
          <Route path="/appointments" element={<AppointmentsCalendar />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
     
  )
}

export default App
