import { BrowserRouter, Routes, Route } from "react-router-dom";
import Vet from "./pages/Vet";
import Layout from "./components/Layout";

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route element={<Layout />}>
          <Route path="/vets" element={<Vet />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
     
  )
}

export default App
