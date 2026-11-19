import { NavLink , Outlet, useLocation} from "react-router-dom";



const navItems = [
    { label: "Home", path: "/" },
    { label: "Vets", path: "/vets" },
    { label: "Owners", path: "/owners" },
    { label: "Pets", path: "/pets" },
    { label: "Appointments", path: "/appointments" },
];



export default function Layout() {

    const location = useLocation();


    const pathSegments = location.pathname.split("/").filter(Boolean);
    const currentPageTitle =
        pathSegments.length === 0
        ? "Dashboard"
        : pathSegments[0].charAt(0).toUpperCase() + pathSegments[0].slice(1)
    ;

    return (

    <div className="min-h-screen flex flex-col bg-slate-50 text-slate-800">
        {/* Header */}
        <header className="sticky top-0 z-30 bg-white border-b border-slate-200 shadow-xs">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 h-16 flex items-center justify-between">
                {/* Logo */}
                <div className="flex items-center gap-2.5">
                    <div className="w-8 h-8 rounded-lg bg-teal-600 flex items-center justify-center text-white font-bold text-lg">
                        V
                    </div>
                    <span className="font-bold text-lg tracking-tight text-slate-900">
                        VetSystem
                    </span>
                </div>


                {/* Navigation */}
                <nav className="flex items-center gap-1 sm:gap-2 overflow-x-auto">
                    {navItems.map((item) => (
                    <NavLink
                        key={item.path}
                        to={item.path}
                        end={item.path === "/"}
                        className={({ isActive }) =>
                        `px-3 py-2 rounded-md text-sm font-medium transition-colors whitespace-nowrap ${
                            isActive
                            ? "bg-teal-50 text-teal-700"
                            : "text-slate-600 hover:text-slate-900 hover:bg-slate-100"
                        }`
                        }
                    >
                        {item.label}
                    </NavLink>
                    ))}
                </nav>
            </div>
        </header>

        {/* Page Context & Breadcrumbs Bar */}
        <div className="bg-white border-b border-slate-200">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex flex-col sm:flex-row sm:items-center sm:justify-between gap-2">
          <div>
            <nav className="text-xs text-slate-500 flex items-center gap-1.5 mb-1">
              <span>VetCare</span>
              <span>/</span>
              <span className="text-slate-700 font-medium capitalize">
                {pathSegments.length > 0 ? pathSegments.join(" / ") : "Overview"}
              </span>
            </nav>
            <h1 className="text-xl font-semibold text-slate-900">
              {currentPageTitle}
            </h1>
          </div>
        </div>
      </div>
      {/* 3. Main Content Workspace */}
      <main className="flex-1 max-w-7xl w-full mx-auto px-4 sm:px-6 lg:px-8 py-6">
        <Outlet />
      </main>

      {/* Footer */}
      <footer className="bg-white border-t border-slate-200 mt-auto">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4 flex flex-col sm:flex-row items-center justify-between gap-2 text-xs text-slate-500">
          <p>© {new Date().getFullYear()} VetCare System. All rights reserved.</p>
          <div className="flex items-center gap-2">
            <span className="w-2 h-2 rounded-full bg-emerald-500"></span>
            <span>System Online</span>
          </div>
        </div>
      </footer>
                    
      {/* Global Toast Notifications */}
        <div> Toast Notifications</div>
    </div>
  );
}