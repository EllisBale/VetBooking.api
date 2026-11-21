import React from "react";

interface ContentCardProps {
  title?: string;
  subtitle?: string;
  children: React.ReactNode;
  actions?: React.ReactNode;
  className?: string;
}

export default function ContentCard({
  title,
  subtitle,
  children,
  actions,
  className = "",
}: ContentCardProps) {
  return (
    <div className={`bg-white rounded-xl border border-slate-200 shadow-xs p-5 transition-shadow hover:shadow-md ${className}`}>
      {/* Card Header */}
      {(title || subtitle || actions) && (
        <div className="flex items-center justify-between pb-3 mb-3 border-b border-slate-100">
          <div className="grid gap-1">
            {title && <h3 className="font-semibold text-slate-900 text-lg">{title}</h3>}
            {subtitle && <p className=" text-slate-700 font-medium mt-0.5 ">{subtitle}</p>}
            {actions && <div className="flex items-center gap-2">{actions}</div>}
        </div>
        </div>
      )}

      {/* Card Body */}
      <div className="text-slate-700 text-sm">
        {children}
      </div>
    </div>
  );
}