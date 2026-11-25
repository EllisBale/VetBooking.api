import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/react/daygrid";
import timeGridPlugin from "@fullcalendar/react/timegrid";
import themePlugin from "@fullcalendar/react/themes/monarch";
import listPlugin from "@fullcalendar/react/list";

import "@fullcalendar/react/skeleton.css";
import "@fullcalendar/react/themes/monarch/theme.css";
import "@fullcalendar/react/themes/monarch/palettes/purple.css";

export default function CalendarList() {
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
                events={[
                    { title: "Vet Checkup: Buddy", date: "2026-06-01" },
                    { title: "Surgery: Max", date: "2026-06-02" },
                ]}
            />
        </div>
    );
}