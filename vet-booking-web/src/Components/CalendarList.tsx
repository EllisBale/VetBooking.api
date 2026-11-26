import { useEffect, useState } from "react";
import { getJson } from "../api";


import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/react/daygrid";
import timeGridPlugin from "@fullcalendar/react/timegrid";
import themePlugin from "@fullcalendar/react/themes/monarch";
import listPlugin from "@fullcalendar/react/list";

import "@fullcalendar/react/skeleton.css";
import "@fullcalendar/react/themes/monarch/theme.css";
import "@fullcalendar/react/themes/monarch/palettes/purple.css";


import type { AppointmentSummaryResponse } from "../types";
import AppointmentModal from "./AppointmentModal";
import { useNavigate } from "react-router-dom";

export default function CalendarList() {
    const navigate = useNavigate();
    const [appointment, setAppointment] = useState<any[]>([]);
    const [error, setError] = useState<string | null>(null);


    const [selectedAppointment, setSelectedAppointment] = useState<AppointmentSummaryResponse | null >(null);

    useEffect(() => {
        async function fetchAppointments() {

            try {
                const data = await getJson<AppointmentSummaryResponse[]>("/appointment");

                const formattedEvents = data.map(app => ( {
                    id: app.id,
                    title: `Vet Visit: ${app.pet.name} (${app.appointmentType.name})`,
                    start: app.startUtc,
                    end: app.endUtc,

                    extendedProps: {
                        id: app.id,
                        petName: app.pet.name,
                        typeName: app.appointmentType.name,
                        room: app.examRoom,
                        owner: app.ownerName,
                        vetName: app.vetName,
                        status: app.status,
                        price: app.appointmentType.price
                    }
                }));

                setAppointment(formattedEvents);
            }
            catch (e: any) {
                setError(e.message);
            }
        }

        fetchAppointments();
    }, []);

    const handleEventClick = (clickInfo: any) => {
        setSelectedAppointment(clickInfo.event);
    };
 
    return (
        <div className="p-4 max-w-8xl mx-auto rounded-xl shadow-md">
            <FullCalendar
                plugins={[themePlugin, dayGridPlugin, timeGridPlugin, listPlugin]}
                initialView="dayGridMonth"
                weekends={true}
                headerToolbar={{
                    start: 'prev,next today',
                    center: 'title',
                    end: 'dayGridMonth,timeGridWeek,listWeek',
                }}
                events= {appointment || error}
                eventClick={handleEventClick}
            />
            {selectedAppointment && (
                <AppointmentModal 
                    item={selectedAppointment}
                    onClose={() => setSelectedAppointment(null)}
                    onViewDetails={(id) => navigate(`/appointments/${id}`)}
                />
            )}
        </div>
    );
}