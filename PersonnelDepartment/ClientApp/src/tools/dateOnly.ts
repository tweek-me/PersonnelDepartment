import { format } from 'date-fns';
import { DateTimeFormatType } from './dates';

export class DateOnly {
    constructor(
        public readonly date: Date
    ) { }

    public addDays(days: number): DateOnly {
        return new DateOnly(this.date.addDays(days));
    }

    public static fromString(str: string): DateOnly {
        // Ожидаем формат dd.MM.yyyy
        const parts = str.split('.');
        const year = parts[2];
        const month = parts[1];
        const day = parts[0];

        const validDateAsString = `${year}-${month}-${day}`;
        const validDate = new Date(validDateAsString);

        return new DateOnly(validDate);
    }

    public static fromDate(date: Date): DateOnly {
        return new DateOnly(date);
    }

    public static get today(): DateOnly {
        return new DateOnly(new Date());
    }

    public static get beginOfMonth(): DateOnly {
        const beginOfMonth = new Date(new Date().setDate(1));
        return new DateOnly(beginOfMonth);
    }

    toString = () => this.date.formatWith(DateTimeFormatType.date);
    toJSON = () => format(this.date, DateTimeFormatType.getFormat(DateTimeFormatType.date));
}

export function mapToDateOnly(date: any) {
    const dateAsString = date as string;
    return DateOnly.fromString(dateAsString);
}
