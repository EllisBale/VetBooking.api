import { BrowserRouter, Routes, Route } from "react-router-dom";
import Vet from "./pages/Vet";

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/vets" element={<Vet />}></Route>
      </Routes>
    </BrowserRouter>
     
  )
}

export default App
