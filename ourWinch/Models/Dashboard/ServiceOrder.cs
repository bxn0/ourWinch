using System;
using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models.Dashboard
{
    /// <summary>
    /// Represents a service order in the system, containing all the necessary details about a service request.
    /// </summary>
    public class ServiceOrder
    {
        /// <summary>
        /// Gets or sets the unique identifier for the ServiceOrder.
        /// </summary>
        /// <value>
        /// The service order identifier.
        /// </value>
        public int ServiceOrderId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer. This field is required.
        /// </summary>
        /// <value>
        /// The fornavn.
        /// </value>
        [Required]
        public string? Fornavn { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer. This field is required.
        /// </summary>
        /// <value>
        /// The etternavn.
        /// </value>
        [Required]
        public string? Etternavn { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the customer.
        /// </summary>
        /// <value>
        /// The mobil no.
        /// </value>
        public string? MobilNo { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer. This field is required.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the address for service delivery. This field is required.
        /// </summary>
        /// <value>
        /// The adresse.
        /// </value>
        [Required]
        public string? Adresse { get; set; }

        /// <summary>
        /// Gets or sets the description of the fault reported by the customer. This field is required.
        /// </summary>
        /// <value>
        /// The feilbeskrivelse.
        /// </value>
        [Required]
        public string? Feilbeskrivelse { get; set; }

        /// <summary>
        /// Gets or sets the order number associated with this service order.
        /// </summary>
        /// <value>
        /// The ordrenummer.
        /// </value>
        public int Ordrenummer { get; set; }

        /// <summary>
        /// Gets or sets the type of product being serviced.
        /// </summary>
        /// <value>
        /// The produkttype.
        /// </value>
        public string? Produkttype { get; set; }

        /// <summary>
        /// Gets or sets the serial number of the product being serviced. This field is required.
        /// </summary>
        /// <value>
        /// The serienummer.
        /// </value>
        [Required]
        public string? Serienummer { get; set; }

        /// <summary>
        /// Gets or sets the date when the service was received. This field is required.
        /// </summary>
        /// <value>
        /// The mottatt dato.
        /// </value>
        [Required]
        public DateTime MottattDato { get; set; }

        /// <summary>
        /// Gets or sets the year model of the product being serviced.
        /// </summary>
        /// <value>
        /// The årsmodell.
        /// </value>
        public string? Årsmodell { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service is under warranty. This field is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if garanti; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Garanti { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a service is requested.
        /// </summary>
        /// <value>
        ///   <c>true</c> if servis; otherwise, <c>false</c>.
        /// </value>
        public bool Servis { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a repair is requested.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reperasjon; otherwise, <c>false</c>.
        /// </value>
        public bool Reperasjon { get; set; }

        /// <summary>
        /// Gets or sets comments from the customer regarding the service order.
        /// </summary>
        /// <value>
        /// The kommentar fra kunde.
        /// </value>
        public string? KommentarFraKunde { get; set; }

        /// <summary>
        /// Gets or sets the current status of the service order. This property is set internally.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string? Status { get; internal set; }

        /// <summary>
        /// Gets or sets whether a service form has been created. Default is "Nei" (No).
        /// </summary>
        /// <value>
        /// The service skjema.
        /// </value>
        public string ServiceSkjema { get; set; } = "Nei";
    }
}
