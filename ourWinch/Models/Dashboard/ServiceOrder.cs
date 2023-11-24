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
        public int ServiceOrderId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer. This field is required.
        /// </summary>
        [Required]
        public string? Fornavn { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer. This field is required.
        /// </summary>
        [Required]
        public string? Etternavn { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the customer.
        /// </summary>
        public string? MobilNo { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer. This field is required.
        /// </summary>
        [Required]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the address for service delivery. This field is required.
        /// </summary>
        [Required]
        public string? Adresse { get; set; }

        /// <summary>
        /// Gets or sets the description of the fault reported by the customer. This field is required.
        /// </summary>
        [Required]
        public string? Feilbeskrivelse { get; set; }

        /// <summary>
        /// Gets or sets the order number associated with this service order.
        /// </summary>
        public int Ordrenummer { get; set; }

        /// <summary>
        /// Gets or sets the type of product being serviced.
        /// </summary>
        public string? Produkttype { get; set; }

        /// <summary>
        /// Gets or sets the serial number of the product being serviced. This field is required.
        /// </summary>
        [Required]
        public string? Serienummer { get; set; }

        /// <summary>
        /// Gets or sets the date when the service was received. This field is required.
        /// </summary>
        [Required]
        public DateTime MottattDato { get; set; }

        /// <summary>
        /// Gets or sets the year model of the product being serviced.
        /// </summary>
        public string? Årsmodell { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the service is under warranty. This field is required.
        /// </summary>
        [Required]
        public bool Garanti { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a service is requested.
        /// </summary>
        public bool Servis { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a repair is requested.
        /// </summary>
        public bool Reperasjon { get; set; }

        /// <summary>
        /// Gets or sets comments from the customer regarding the service order.
        /// </summary>
        public string? KommentarFraKunde { get; set; }

        /// <summary>
        /// Gets or sets the current status of the service order. This property is set internally.
        /// </summary>
        public string? Status { get; internal set; }

        /// <summary>
        /// Gets or sets whether a service form has been created. Default is "Nei" (No).
        /// </summary>
        public string ServiceSkjema { get; set; } = "Nei";
    }
}
