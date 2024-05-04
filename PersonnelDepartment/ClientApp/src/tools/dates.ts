import { DateOnly } from "./dateOnly";
import { NeverUnreachable } from "./errors/neverUnreachable";

export { };

declare global {
    interface Date {
        formatWith(format: DateTimeFormatType): string;
        formatDuration(): string;
        toLocal(force?: boolean): Date;
        beginOfDay(): Date;
        endOfDay(): Date;
        beginOfWeek(): Date;
        endOfWeek(): Date;
        beginOfMonth(): Date;
        endOfMonth(): Date;
        addDays(days: number): Date;
        toDateOnly(): DateOnly;
    }
}

Date.prototype.formatWith = function (formatType: DateTimeFormatType): string {
    const format = DateTimeFormatType.getFormat(formatType);

    // http://unicode.org/reports/tr35/tr35-dates.html#dfst-era

    const getSecondFormat = (): '2-digit' | 'numeric' | undefined => {
        if (format.includes('ss')) return '2-digit';
        if (format.includes('s')) return 'numeric';
        return undefined;
    }

    const getMinuteFormat = (): '2-digit' | 'numeric' | undefined => {
        if (format.includes('mm')) return '2-digit';
        if (format.includes('m')) return 'numeric';
        return undefined;
    }

    const getHourFormat = (): '2-digit' | 'numeric' | undefined => {
        if (format.includes('hh') || format.includes('HH')) return '2-digit';
        if (format.includes('h') || format.includes('H')) return 'numeric';
        return undefined;
    }

    const getHour12Format = (): boolean | undefined => {
        if (format.includes('h')) return true;
        if (format.includes('H')) return false;
        return undefined;
    }

    const getDayFormat = (): '2-digit' | 'numeric' | undefined => {
        if (format.includes('dd')) return '2-digit';
        if (format.includes('d')) return 'numeric';
        return undefined;
    }

    const getMonthFormat = (): 'numeric' | '2-digit' | 'long' | 'short' | 'narrow' | undefined => {
        if (format.includes('MMMMM')) return 'narrow';
        if (format.includes('MMMM')) return 'long';
        if (format.includes('MMM')) return 'short';
        if (format.includes('MM')) return '2-digit';
        if (format.includes('M')) return 'numeric';
        return undefined;
    };

    const getYearFormat = (): '2-digit' | 'numeric' | undefined => {
        if (format.includes('yyyy')) return 'numeric';
        if (format.includes('yy')) return '2-digit';
        return undefined;
    }

    const hour12Format = getHour12Format() ?? false;
    const dateTimeFormat = Intl.DateTimeFormat('ru', {
        second: getSecondFormat(),
        minute: getMinuteFormat(),
        hour: getHourFormat(),
        hour12: hour12Format,
        day: getDayFormat(),
        month: getMonthFormat(),
        year: getYearFormat()
    });

    const dateParts: Intl.DateTimeFormatPart[] = dateTimeFormat.formatToParts(this);

    let formatted = format;
    dateParts.forEach(part => {
        switch (part.type) {
            case "second": formatted = formatted.replace(/[s]+/g, part.value); break;
            case "minute": formatted = formatted.replace(/[m]+/g, part.value); break;
            case "hour":
                if (hour12Format) formatted = formatted.replace(/[h]+/g, part.value);
                if (!hour12Format) formatted = formatted.replace(/[H]+/g, part.value);
                break;
            case "day": formatted = formatted.replace(/[d]+/g, part.value); break;
            case "month": formatted = formatted.replace(/[M]+/g, part.value); break;
            case "year": formatted = formatted.replace(/[y]+/g, part.value); break;

            case "era":
            case "dayPeriod":
            case "literal":
            case "timeZoneName":
            case "weekday":
                break;
        }
    });

    return formatted;
}

Date.prototype.formatDuration = function (): string {
    let ms = this.getTime();

    if (ms < 0) ms = -ms;

    const time = {
        д: Math.floor(ms / 86400000),
        ч: Math.floor(ms / 3600000) % 24,
        м: Math.floor(ms / 60000) % 60,
        с: Math.floor(ms / 1000) % 60
    };

    return Object.entries(time)
        .filter(val => val[1] !== 0)
        .map(([key, val]) => `${val}${key}`)
        .join(' ');
}

Date.prototype.toLocal = function (force?: boolean): Date {
    if (!force && this.getTimezoneOffset() != 0) return this;

    const localDateOffset = new Date().getTimezoneOffset();
    return new Date(this.getTime() - localDateOffset * 60 * 1000);
}

Date.prototype.beginOfDay = function (): Date {
    const date = new Date(this.getTime());
    date.setHours(0, 0, 0, 0);
    return date;
}

Date.prototype.endOfDay = function (): Date {
    const date = new Date(this.getTime());
    date.setHours(23, 59, 59, 999);
    return date;
}

Date.prototype.beginOfMonth = function (): Date {
    return new Date(this.getFullYear(), this.getMonth(), 1);
}

Date.prototype.endOfMonth = function (): Date {
    return new Date(this.getFullYear(), this.getMonth() + 1, 0);
}

Date.prototype.addDays = function (days: number): Date {
    const newDate = new Date(this);
    newDate.setDate(newDate.getDate() + days);
    return newDate;
}

Date.prototype.toDateOnly = function (): DateOnly {
    return new DateOnly(this);
}


export enum DateTimeFormatType {
    date,
    dateWithTime,
    dateWithShortTime,
    dateWithShortYear,
    shortTime,
    longTime,
    monthAndYear,
    T
}

export namespace DateTimeFormatType {
    export function getFormat(type: DateTimeFormatType): string {
        switch (type) {
            case DateTimeFormatType.date: return 'dd.MM.yyyy';
            case DateTimeFormatType.dateWithTime: return 'dd.MM.yyyy HH:mm:ss';
            case DateTimeFormatType.dateWithShortTime: return 'dd.MM.yyyy HH:mm';
            case DateTimeFormatType.dateWithShortYear: return 'dd.MM.yy';
            case DateTimeFormatType.shortTime: return 'HH:mm';
            case DateTimeFormatType.longTime: return 'HH:mm:ss';
            case DateTimeFormatType.monthAndYear: return 'MM.yyyy';
            case DateTimeFormatType.T: return "yyyy-MM-dd'T'HH:mm:ss.SSS";
            default: throw new NeverUnreachable(type);
        }
    }
}
